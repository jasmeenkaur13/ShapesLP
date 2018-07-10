// generate shape according to the result returned from rest
function generateShapeInCanvas(result) {
    var shape = result.ShapeType;
    switch (shape.toLowerCase()) {
        case "triangle": var size = parseFloat(result.Dimensions[0].Value);
            generateCanvas(3, size);
            break;
        case "hexagon": var size = parseFloat(result.Dimensions[0].Value);
            generateCanvas(6, size);
            break;
        case "octagon": var size = parseFloat(result.Dimensions[0].Value);
            generateCanvas(8, size);
            break;
        case "heptagon": var size = parseFloat(result.Dimensions[0].Value);
            generateCanvas(7, size);
            break;
        case "pentagon": var size = parseFloat(result.Dimensions[0].Value);
            generateCanvas(5, size);
            break; 
        case "square": var size = parseFloat(result.Dimensions[0].Value);
            drawSquareRectangle(size, size);
            break;
        case "rectangle": var width, height;
            if (result.Dimensions[0].Key.toLowerCase() === "length" || result.Dimensions[0].Key.toLowerCase() === "height") {
                height = parseFloat(result.Dimensions[0].Value);
                width = parseFloat(result.Dimensions[1].Value);
            } else {
                height = parseFloat(result.Dimensions[1].Value)
                width = parseFloat(result.Dimensions[0].Value);}
            drawSquareRectangle(width, height);
            break;
        case "circle": var size = parseFloat(result.Dimensions[0].Value);
            drawCircle(size);
            break;
        case "oval": var size = parseFloat(result.Dimensions[0].Value);
            drawOval(size);
            break;

        default:
    }
}

// draw circle
function drawCircle(radius) {
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    ctx.beginPath();
    ctx.arc(radius+10, radius+ 10, radius, 0, 2 * Math.PI);
    ctx.stroke();
}

// draw oval
function drawOval(radius) {
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    ctx.lineWidth = 1;
    ctx.scale(1, 0.5);
    ctx.beginPath();
    ctx.arc(radius + 10, radius + 10, radius, 0, Math.PI * 2);
    ctx.stroke();
    ctx.scale(1, 2);
}

// draw Square and rectangle
function drawSquareRectangle(width,height) {
    var canvas = document.getElementById('myCanvas');
    if (canvas.getContext) {
        var ctx = canvas.getContext('2d');
        ctx.strokeRect(10, 10, width, height);
    }
}

// generate polygon
function generateCanvas(noOfSides, size) {
    var c = document.getElementById("myCanvas");
    var cxt = c.getContext("2d");
    var numberOfSides = noOfSides,
        size = size,
        Xcenter = size + 10,
        Ycenter = size + 10;

    cxt.beginPath();
    cxt.moveTo(Xcenter + size * Math.cos(0), Ycenter + size * Math.sin(0));

    for (var i = 1; i <= numberOfSides; i += 1) {
        cxt.lineTo(Xcenter + size * Math.cos(i * 2 * Math.PI / numberOfSides), Ycenter + size * Math.sin(i * 2 * Math.PI / numberOfSides));
    }

    cxt.strokeStyle = "#000000";
    cxt.lineWidth = 1;
    cxt.stroke();
}

// Responsible to hit the Rest API using XMLHttpRequest
function evaluateShape() {
    var can = document.getElementById("myCanvas");
    var context = can.getContext("2d");
    context.clearRect(0, 0, context.canvas.width, context.canvas.height);
    document.getElementById("Error").innerHTML = "";

    var url = "http://localhost:50825/NLPProcessor.svc/Shapes";

    var inputText = {};
    inputText.data = document.getElementById("inputText").value;
    var json = JSON.stringify(inputText);

    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    xhr.onload = function () {
        var result = JSON.parse(xhr.responseText);
        if (xhr.readyState == 4 && xhr.status == "200") {
            generateShapeInCanvas(result.EvaluateShapeDetailsResult);
        } else {
            document.getElementById("Error").innerHTML = result;
        }
    }
    xhr.send(json);
    }