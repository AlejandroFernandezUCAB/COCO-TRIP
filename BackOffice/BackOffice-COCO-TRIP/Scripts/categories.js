$(document).ready(function () {
  $('#example').DataTable();

  $(".ver-categoria").click(function () {
    $("#mostrarCategoria").modal();
  })

  $(":checkbox").change(function () {
    showLoader();
    setTimeout(hideLoader, 5000);
  })
});



function showLoader() {
  console.log("show")
  document.getElementById("loader").style.display = "block";
  document.getElementById("fondo-opaco").style.display = "block";
}

function hideLoader() {
  document.getElementById("loader").style.display = "none";
  document.getElementById("fondo-opaco").style.display = "none";
}
