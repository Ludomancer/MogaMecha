var rotation : float = 282.8941;
var bloomAmount: float = 0.77;
var cameraLeft: GameObject;
var cameraRight: GameObject;

function Update () {
gameObject.Find(cameraLeft.name).GetComponent(BloomAndLensFlares).bloomThreshhold = bloomAmount;
gameObject.Find(cameraRight.name).GetComponent(BloomAndLensFlares).bloomThreshhold = bloomAmount;
gameObject.transform.rotation = Quaternion.Euler(270,rotation,0);
rotation += 0.01;

if(rotation/360 >=1 && rotation % 360 > 77 && rotation % 360 <150 && bloomAmount<1.1){
bloomAmount+=0.002;
}

else if(rotation/360 >=1 && rotation % 360 > 150 && bloomAmount>0.77){
bloomAmount-=0.0001;
}
//print( rotation % 360);
}