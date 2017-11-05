const STATUS = {
  0: "DESACTIVADO",
  1: "ACTIVADO"
}

$(document).ready(function () {
  $('#example').DataTable();

  $(".ver-categoria").click(function () {
    var name = $(this).attr("data-name");
    var desription = $(this).attr("data-description");
    showModal(name, desription);
   
  })

  $(":checkbox").change(function () {

    var categoriaId = $(this).attr("data-categoria-id");
    var categoriaName = $(this).attr("data-categoria-name");
    var status = $(this).is(":checked");
    activarDeactivarAjax(categoriaId, status, categoriaName).then(response => {
      if (!response) {
        $(this).prop("checked", !status);
      }
    })

  });
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


function activarDeactivarAjax(id, status, name) {

  return new Promise(function (resolve) {
    var jsonCategoria = {
      Id: id,
      Status: status
    };
    var finalizacion;
    showLoader();
    $.ajax({
      url: "/Categories/ChangeStatus",
      type: "POST",
      contentType: "application/json;charset=utf-8",
      dataType: "json",
      data: JSON.stringify(jsonCategoria)
    }).done(function (data) {
      status = status ? 1 : 0;
      swal({
        //position: 'top-right',
        type: "success",
        //toast: true,
        title: "EXITO!",
        text: "Se ha actualizado el estatus de " + name + " a " + STATUS[status],
        timer: 4000
      });
      hideLoader();
      resolve(true);
    }).fail(function (error) {
      swal({
        //position: 'top-right',
        type: "error",
        title: "ERROR!",
        text: "No se pudo actualizar el estatus",
        showCancelButton: false,
        timer: 4000
      });
      hideLoader();
      resolve(false);
    })
  })
  
}

function showModal(name, description) {
  $("#categories-name-details").val(name);
  $("#categories-description-details").val(description);
  $("#mostrarCategoria").modal();
}
