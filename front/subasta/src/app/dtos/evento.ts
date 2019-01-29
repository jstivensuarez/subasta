import { Municipio } from "./municipio";

export class Evento {

    eventoId: number;
    descripcion: string;
    fechaInicio: Date;
    fechaFin: Date;
    municipioId: number;
    municipio: Municipio;
    
    constructor(){}
}
