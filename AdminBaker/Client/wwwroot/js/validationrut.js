window.validacionRut = (rutCompleto) => {
    if (!/^[0-9]+-[0-9kK]{1}$/.test(rutCompleto))
        return false;
    const tmp = rutCompleto.split('-');
    var digv = tmp[1];
    const rut = tmp[0];
    if (digv == 'K') digv = 'k';
    return (dv(rut) == digv);
}

function dv(t) {
    var m = 0, s = 1;
    for (; t; t = Math.floor(t / 10))
        s = (s + t % 10 * (9 - m++ % 6)) % 11;
    return s ? s - 1 : 'k';
}