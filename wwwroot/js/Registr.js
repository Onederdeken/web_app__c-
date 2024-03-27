



function RoleUser(){
    let forma = document.getElementById('main-form')
    let element = document.getElementById('RoleSelect');
    var UserForm = document.getElementById('UserForm');
    var SellerForm = document.getElementById('SellerForm');
    if(element.value == 'Пользователь'){
        if(UserForm.style.display!='none' ){
            return
        }
        else if(SellerForm.style.display!='none'){
            SellerForm.style.display='none';
            UserForm.style.display='block'; 
        }
        else{
            UserForm.style.display='block';
        }
    }
    else if(element.value =='Продавец'){
        if(SellerForm.style.display!='none' ){
            return
        }
        else if(UserForm.style.display!='none'){
            UserForm.style.display='none';
            SellerForm.style.display='block';
        }
        else{
            SellerForm.style.display='block';
        }
    }
}

var count = 0;
function InputTelephone(ex){
    if(count==3 | count==6 | count==8){
        ex.value+="-";
    }
    count++;
}

function CliclTelephone(ex){
    if(ex.value !=''){
        return;
    }
    else{
        count=1;
        ex.value+="+7-";
    }
}

function CheckError(ex){
    if(ex.value != null){
        ex.style.removeProperty('display');
    }
}

