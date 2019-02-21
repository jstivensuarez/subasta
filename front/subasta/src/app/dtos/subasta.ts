import { Time } from "@angular/common";
import { Evento } from "./evento";
import { Lote } from "./lote";

export class Subasta {

    subastaId: number;
    descripcion: string;
    horaInicio: Date;
    horaFin: Date;
    horaInicioAux: Date;
    horaFinAux: Date;
    valorAnticipo: number;
    precioInicial: number;
    eventoId: number;
    evento: Evento;
    totalSegundos: number;
    estadoSolicitud: string;
    lotesDto: Lote[];
    constructor(){}
}