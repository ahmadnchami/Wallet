(function () {
    var map;
    var latField = document.getElementById("Latitude");
    var lngField = document.getElementById("Longitude");

    function initMap() {
        var mapOptions = {
            center: { lat: parseFloat(latField.value), lng: parseFloat(lngField.value) },
            zoom: 18
        };

        var applyInit = function (options) {

            map = new google.maps.Map(document.getElementById('map'), options);

            var input = document.getElementById('places-search');

            var searchBox = new google.maps.places.SearchBox(input);
            map.controls[google.maps.ControlPosition.TOP_RIGHT].push(input);

            map.addListener('bounds_changed', function () {
                searchBox.setBounds(map.getBounds());
            });


            searchBox.addListener('places_changed', function () {
                var places = searchBox.getPlaces();

                if (places == null) return;


                var bounds = new google.maps.LatLngBounds();

                places.forEach(function (place) {
                    if (!place.geometry) return;
                    if (place.geometry.viewport) {
                        bounds.union(place.geometry.viewport);
                    } else {
                        bounds.extend(place.geometry.location);
                    }
                });

                map.fitBounds(bounds);
            });

            $('<div/>').addClass('centerMarker').appendTo(map.getDiv());

            google.maps.event.addListener(map, "center_changed", function () {
                var lat = map.getCenter().lat();
                var lng = map.getCenter().lng();

                latField.value = lat;
                lngField.value = lng;
            });
        }

        //create
        if (isNaN(parseFloat(mapOptions.center.lat))) {

            var handleLocationError = function () {

                mapOptions.center = { lat: 33.85898374068318, lng: 35.48037319882814 };
                mapOptions.zoom = 8;


                latField.value = mapOptions.center.lat;
                lngField.value = mapOptions.center.lng;

                applyInit(mapOptions);
            };

            // Try HTML5 geolocation.
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };

                    mapOptions.center = pos;

                    applyInit(mapOptions);

                }, function () {
                    handleLocationError();
                });
            } else {
                // Browser doesn't support Geolocation
                handleLocationError();
            }

        } else {
            applyInit(mapOptions);
        }

    }

    window.initMap = initMap;
})();