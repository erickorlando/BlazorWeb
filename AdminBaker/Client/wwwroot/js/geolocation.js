window.blazorGetGeolocation = () => {
    return new Promise((resolve, reject) => {
        if ("geolocation" in navigator) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    resolve({
                        latitude: position.coords.latitude,
                        longitude: position.coords.longitude
                    });
                },
                (error) => {
                    reject(error.message);
                },
                {
                    enableHighAccuracy: true,
                    timeout: 10000
                }
            );
        } else {
            reject("La geolocalización no está disponible en este navegador.");
        }
    });
};