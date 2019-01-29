import { Time } from "@angular/common";
import { Evento } from "./evento";

export class Subasta {

    subastaId: number;
    fechaLimite: Date;
    horaInicio: Date;
    horaFin: Date;
    horaInicioAux: Date;
    horaFinAux: Date;
    valorAnticipo: number;
    precioInicial: number;
    eventoId: number;
    evento: Evento;
    
    constructor(){}
}