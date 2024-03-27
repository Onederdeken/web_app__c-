function CustomAlert() {
    this.render = function(dialog) {
      var winW = window.innerWidth;
      var winH = window.innerHeight;
      var dialogoverlay = document.getElementById('dialogoverlay');
      var dialogbox = document.getElementById('dialogbox');
      dialogoverlay.style.display = "flex";
      dialogoverlay.style.height = winH + "px";
      dialogbox.style.left = (winW / 2) - (550 * .5) + "px";
      dialogbox.style.top = "100px";
      dialogbox.style.display = "flex";
      dialogbox.style.flexDirection = "column";
      document.getElementById('dialogboxhead').innerHTML = "";
      document.getElementById('dialogboxbody').innerHTML = dialog;
      var button = document.createElement("button");
      button.type="button";
      button.className='button_add';
      button.onclick="Alert.ok()";
      document.getElementById('dialogboxfoot').innerHTML = button;
    }
    this.ok = function() {
      document.getElementById('dialogbox').style.display = "none";
      document.getElementById('dialogoverlay').style.display = "none";
    }
  }





function CheckBuy(a, b){
  var Alert = new CustomAlert();
  if(a < b | a == b){
    Alert.render('Оплата прошла успешно');
  }
  else{
  }

}