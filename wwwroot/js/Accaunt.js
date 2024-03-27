
function CreateFormAddedProducts(){
    var body = document.getElementsByClassName("Form");
    var form = document.createElement("form");
    form.setAttribute(location.href, '@Url.Action("ListProducts","Accaunt")');
    window.location.href

}

function Money(){
    let moneydiv = document.querySelector('.Money');
    let inputmoney = document.createElement('input');
    let buttonmoney = document.createElement('button');
    inputmoney.placeholder='Введите сколько ДЭНЕГ вам надо';
    inputmoney.name='money';
    inputmoney.autocomplete="off"
    inputmoney.min="0";
    inputmoney.value=0;
    inputmoney.step=1;
    inputmoney.className="input_vvod";
    inputmoney.type='number';
    buttonmoney.type='submit';
    buttonmoney.textContent='Send';
    buttonmoney.className='buttons_for_money';
    let button_add_money = document.getElementById('button_add_money');
    button_add_money.style.display='none';
    inputmoney.className="butt_money";
    inputmoney.max = 9;
    $('body').on('input', '.butt_money', function(){
        this.value = this.value.replace(/[^0-9\.\,]/g, '');
    });
    $(".butt_money").mask("[0-9]9");
    buttonmoney.addEventListener("click", ()=> location.href='AddMoney');
    moneydiv.append(inputmoney);
    moneydiv.append(buttonmoney);

}
function ChangePhoto(){
    alert("gwrrh");
}
