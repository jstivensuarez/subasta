import { Municipio } from "./municipio";
import { Subasta } from "./subasta";

export class Evento {

    eventoId: number;
    descripcion: string;
    fechaInicio: Date;
    fechaFin: Date;
    municipioId: number;
    municipio: Municipio;
    subastasDto: Subasta[];
    constructor(){}
}
