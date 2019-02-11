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
    pesoPromedio: number;
    valorAnticipo: number;
    precioInicial: number;
    fotoLote: string;
    clienteId: string;
    cliente: Cliente;
    municipioId: number;
    municipio: Municipio;
    subastaId: number;
    subasta: Subasta;
    imagen: File;
    video: string;
    lotesDto: Lote[];

    constructor() { }
}
