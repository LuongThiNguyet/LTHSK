
        function load(){
    var lstC = [
        'AMD','INTEL','IBM','Siemens'
    ];
    console.log(lstC.length);
    var select = document.getElementById('slc');
    for(var i=0;i<lstC.length;i++){
    var opt = document.createElement('option');
    opt.value = i;
    opt.innerHTML = lstC[i];
    select.appendChild(opt);
     document.getElementById("Luu").style.display = 'none';
     document.getElementById("Hang").style.display = 'none';
     document.getElementById("Han").style.display = 'none';
}
        }
    function them(){
    document.getElementById("Luu").style.display = 'inline';
     document.getElementById("Hang").style.display = 'inline';
     document.getElementById("Han").style.display = 'inline';
     document.getElementById("Them").style.display = 'none';
    
     
}
function Luu(){
    var select = document.getElementById('slc');
    var  L = slc.options.length ;
        for(var i=L;i<=L;i++){
    var opt = document.createElement('option');
    var text= document.getElementById("Hang");
    opt.value = i;
    opt.innerHTML = text.value;
    select.appendChild(opt);
    }
    console.log(L);
    console.log(text.value)
    document.getElementById("Them").style.display = 'inline';
    document.getElementById("Luu").style.display = 'none';
    document.getElementById("Hang").style.display = 'none';
    document.getElementById("Han").style.display = 'none';
    }
  