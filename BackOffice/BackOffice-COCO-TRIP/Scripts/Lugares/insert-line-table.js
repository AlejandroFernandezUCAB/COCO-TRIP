var select_categoria    = document.getElementById('categoria');
var select_subcategoria = document.getElementById('subcategoria');
var selectedOption;
var selectedOptionTwo;

select_categoria.addEventListener('change',
  function(){
    selectedOption = this.options[select_categoria.selectedIndex];
    console.log(selectedOption.value + ':' + selectedOption.text);
  });

select_subcategoria.addEventListener('change',
  function(){
    selectedOptionTwo = this.options[select_subcategoria.selectedIndex];
    console.log(selectedOptionTwo.value + ': ' + selectedOptionTwo.text);
  });

function agregarCategoria() {
    var categoria 	 = document.getElementById("categoria").value;

    var subcategoria = document.getElementById("subcategoria").value;

    var eliminar 	 = '<a class="btn btn-danger borrar">Eliminar</a>'

    var table = document.getElementById("tabla");
    var row = table.insertRow(1);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    cell1.innerHTML=categoria;
    cell2.innerHTML=subcategoria;
	  cell3.innerHTML=eliminar     
}

function agregarHorario() {
    var dia      = document.getElementById("dia").value;

    var apertura = document.getElementById("apertura").value;

    var cierre   = document.getElementById("cierre").value;

    var eliminar = '<a class="btn btn-danger borrar">Eliminar</a>'
    
    console.log(dia + ' : dia');
    console.log(apertura + ' : apertura');
    console.log(cierre + ' : cierre'); 

    var tabla = document.getElementById("tabla_horario");
    var row   = tabla.insertRow(1);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);
    cell1.innerHTML=dia;
    cell2.innerHTML=apertura;
    cell3.innerHTML=cierre;
    cell4.innerHTML=eliminar    
}

