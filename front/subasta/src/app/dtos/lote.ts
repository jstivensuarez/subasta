import { Cliente } from "./cliente";
import { Municipio } from "./municipio";
import { Subasta } from "./subasta";

export class Lote {

    loteId: string;
    nombre: string;
    descripcion: string;
    cantidadElementos: number;
    pesoTotal: number;
    precioBase: number;
    valorAnticipo: number;
    fotoLote: string;
    clienteId: string;
    cliente: Cliente;
    municipioId: number;
    municipio: Municipio;
    subastaId: number;
    subasta: Subasta;
    imagen: File;
    constructor() { }
}
