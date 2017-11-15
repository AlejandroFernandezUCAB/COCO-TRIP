var map;
  function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
      center: {lat: document.getElementById('latitud').value, lng: document.getElementById('longitud').value},
      zoom: 8
    });
  }