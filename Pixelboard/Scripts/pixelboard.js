$(function () {

    // ------------------------------
    // Server communication 
    // ------------------------------

    // Declare a proxy to reference the hub.
    var hub = $.connection.pixelboardHub;

    // Server sends canvas to client
    hub.client.sendCanvas = function(width, height, contents) {
        updateCanvas(width, height, contents);
    };   

    // Server sends palette to client
    hub.client.sendPalette = function(colors) {
        updatePalette(colors);
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
        setupCanvas();
    }

    var canvasScale = 6;
    setupCanvas = function () {
        var c = $('#pixelboard');
        var ctx = c[0].getContext("2d");
        ctx.scale(canvasScale, canvasScale);
    }



    // -----------------------------------------------
    // Pixelboard API starts here
    // -----------------------------------------------
   

    updateCanvas = function (width, height, contents) {
        var c = $('#pixelboard');
        var ctx = c[0].getContext("2d");
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                drawCanvasPixel(ctx, x, y, contents[y * width + x]);
            }
        }
    }

    drawCanvasPixel = function (ctx, x, y, color) {
        ctx.fillStyle = "rgb(" + color.R + "," + color.G + "," + color.B + ")";
        ctx.fillRect(x, y, 1, 1);
    }

    updatePalette = function (colors) {
        for (var i = 0; i < colors.length; i++) {
            var color = colors[i];
            var colorButton = $('<input type="button" class="palettecolor" value=""/>');
            colorButton[0].style.background = "rgb(" + color.R + "," + color.G + "," + color.B + ")";
            $("#palette").append(colorButton);
        }
    }

});
