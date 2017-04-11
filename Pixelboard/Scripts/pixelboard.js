$(function () {

    // ------------------------------
    // Server communication 
    // ------------------------------

    // Declare a proxy to reference the hub.
    var hub = $.connection.pixelboardHub;

    // Server sends canvas to client
    hub.client.sendCanvas = function (width, height, contents) {
        canvasWidth = width;
        canvasHeight = height;
        canvasContents = contents;
        updateCanvas();
    };   

    // Server sends palette to client
    hub.client.sendPalette = function (colors) {
        paletteColors = colors;
        updatePalette();
    };

    // Server sends cooldown in seconds to client
    hub.client.sendCooldown = function (cooldownDuration) {
        startCooldown(cooldownDuration);
    };

    // Server sends changed pixel to client
    hub.client.broadcastPixel = function (x, y, color) {
        updatePixel(x, y, color);
    };


    // Start the connection.
    $.connection.hub.start().done(function () {
        setupUI();
        // Initial request of canvas and palette from server
        hub.server.requestCanvas();
        hub.server.requestPalette();
    });

    // ------------------------
    // UI Setup
    // ------------------------

    setupUI = function () {
        // Bind randomize button click
        $('#randomize').click(function () { hub.server.randomizeCanvas() });
        $('#pixelboard').click(onPixelBoardClick);
        $('#cooldowntimer').toggle(false); // Cooldowntimer is initially hidden
    }

    onPixelBoardClick = function (evt) {
        if (!isCoolDown()) {
            var x = Math.floor(evt.offsetX / canvasScale);
            var y = Math.floor(evt.offsetY / canvasScale);
            hub.server.putPixel(x, y, selectedColor);
        }
        else {
            // Shake the timer when clicking during cooldown
            // to emphasize it.
            $('#cooldowntimer').animate({ left: "+=5" }, 100).animate({ left: "-=10" }, 100).animate({ left: "+=5" }, 100);
        }
    }

    isCoolDown = function () {
        return cooldownEndTime > Math.floor(Date.now() / 1000);
    }


    selectPaletteColor = function (colorIndex) {
        selectedColor = colorIndex;
        updatePalette()
    }

    var canvasWidth;
    var canvasHeight;
    var canvasContents;
    var paletteColors;
    var canvasScale = 8;
    var selectedColor = 0;
    var cooldownEndTime = 0;

    // -----------------------------------------------
    // Pixelboard API starts here
    // -----------------------------------------------
   

    updateCanvas = function() {
        var c = $('#pixelboard');
        var ctx = c[0].getContext("2d");
        c[0].width = canvasWidth * canvasScale;
        c[0].height = canvasHeight * canvasScale;
        ctx.scale(canvasScale, canvasScale);        
        for (var y = 0; y < canvasHeight; y++) {
            for (var x = 0; x < canvasWidth; x++) {
                drawCanvasPixel(ctx, x, y, canvasContents[y * canvasWidth + x]);
            }
        }
    }

    drawCanvasPixel = function (ctx, x, y, color) {
        ctx.fillStyle = "rgb(" + color.R + "," + color.G + "," + color.B + ")";
        ctx.fillRect(x, y, 1, 1);
    }

    updatePalette = function () {
        $("#palette").empty();
        for (var i = 0; i < paletteColors.length; i++) {
            var color = paletteColors[i];
            var colorButton = $('<input type="button" class="palettecolor" data-colorindex="'+i+'" onclick="selectPaletteColor('+i+')"/>');
            colorButton[0].style.background = "rgb(" + color.R + "," + color.G + "," + color.B + ")";
            if (i == selectedColor)
            {
                colorButton.addClass("selected");
            }
            $("#palette").append(colorButton);
        }
    }

    updatePixel = function (x, y, color) {        
        canvasContents[y * canvasWidth + x] = color;
        var c = $('#pixelboard');
        var ctx = c[0].getContext("2d")
        drawCanvasPixel(ctx, x, y, color);
    }

    startCooldown = function (cooldownDuration) {
        cooldownEndTime = Math.floor(Date.now() / 1000) + cooldownDuration;
        initializeCooldownClock()
    }


    initializeCooldownClock = function () {
        $('#cooldowntimer').toggle(true);
        var clock = $('#cooldowntimer')[0];        
        var cooldownLeft = cooldownEndTime - Math.floor(Date.now() / 1000);
        clock.innerHTML = cooldownLeft;
        var timeinterval = setInterval(function () {
            var cooldownLeft = cooldownEndTime - Math.floor(Date.now() / 1000);
            clock.innerHTML = cooldownLeft;
            if (cooldownLeft <= 0) {
                $('#cooldowntimer').toggle(false);
                clock.innerHTML = "";
                clearInterval(timeinterval);
            }
        }, 1000);
    }


});
