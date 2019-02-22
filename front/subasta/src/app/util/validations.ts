import { AbstractControl } from '@angular/forms';
export class Validation {

    static MatchPassword(AC: AbstractControl) {
        let password = AC.get('password').value; // to get value in input tag
        let confirmPassword = AC.get('confirmPassword').value; // to get value in input tag
        if (password != confirmPassword) {
            console.log('false');
            AC.get('confirmPassword').setErrors({ MatchPassword: true })
        } else {
            console.log('true');
            return null
        }
    }

    static EventoFechas(AC: AbstractControl) {
        let fechaInicio = AC.get('fechaInicio').value;
        let fechaFin = AC.get('fechaFin').value;
        if (fechaInicio > fechaFin) {
            AC.get('fechaFin').setErrors({ fechaMala: true })
        } else {
            return null
        }
    }

    static SubastaFechas(AC: AbstractControl) {
        let fechaInicio = new Date(AC.get('fechaInicio').value);
        let fechaFin = new Date(AC.get('fechaFin').value);
        if (fechaInicio > fechaFin) {
            AC.get('fechaFin').setErrors({ fechaMala: true })
        } else {
            return null
        }
    }

    static SubastaHoras(AC: AbstractControl) {
        let fechaInicio = new Date(AC.get('fechaInicio').value);
        let horaInicio = Date.parse('01/01/2011 '+AC.get('horaInicio').value);
        let fechaFin = new Date(AC.get('fechaFin').value);
        let horaFin = Date.parse('01/01/2011 '+AC.get('horaFin').value);
        if (fechaInicio && fechaFin && fechaInicio.getTime() === fechaFin.getTime()) {
            if (horaInicio && horaFin && horaInicio > horaFin) {
                AC.get('horaFin').setErrors({ horaMala: true })
            } else {
                return null
            }
        } else {
            return null
        }
    }

    static MatchValidator(AC: AbstractControl) {
        let pass = AC.get('clave').value;
        let passValidation = AC.get('claveRepeat').value;
        if (pass !== passValidation) {
            AC.get('claveRepeat').setErrors({ claveMala: true })
        } else {
            return null
        }
    }

    static MatchValidatorChange(AC: AbstractControl) {
        let pass = AC.get('claveChange').value;
        let passValidation = AC.get('claveRepeat').value;
        if (pass !== passValidation) {
            AC.get('claveRepeat').setErrors({ claveMala: true })
        } else {
            return null
        }
    }
}