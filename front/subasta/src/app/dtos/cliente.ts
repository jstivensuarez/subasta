import { Municipio } from "./municipio";
import { TipoDocumento } from "./tipo-documento";

export class Cliente {

    clienteId: string;
    nombre: string;
    correo: string;
    telefono: string;
    direccion: string;
    representante: string;
    usuario: string;
    clave: string;
    estado: string;
    tipoDocumentoId: number;
    municipioId: number;
    municipio: Municipio;
    tipoDocumento: TipoDocumento;
    constructor(){}
}