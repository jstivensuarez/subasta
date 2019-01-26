import { Municipio } from "./municipio";

export class Cliente {

    clienteId: string;

    nombre: string;

    correo: string;

    telefono: string;

    direccion: string;

    representante: string;

    usuario: string;

    estado: string;

    tipoDocumentoId: number;

    municipioId: number;

    municipio: Municipio;
        
    constructor(){}
}