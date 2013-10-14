var frames : Texture2D[];
var framesPerSecond = 10.0;

function Update () {
    var index : int = Time.time * framesPerSecond;
    index = index % frames.Length;
    renderer.material.mainTexture = frames[index];
}