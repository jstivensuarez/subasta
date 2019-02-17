import { Cliente } from "./cliente";
import { Municipio } from "./municipio";
import { Subasta } from "./subasta";
import { Raza } from "./raza";
import { Clasificacion } from "./clasificacion";
import { Categoria } from "./categoria";

export class Lote {

    loteId: string;
    nombre: string;
    descripcion: string;
    cantidadElementos: number;
    pesoTotal: number;
    precioBase: number;
    pesoPromedio: number;
    precioInicial: number;
    fotoLote: string;
    clienteId: string;
    cliente: Cliente;
    categoriaId: number;
    categoria: Categoria;
    razaId: number;
    raza: Raza;
    clasificacionId: number;
    clasificacion: Clasificacion;
    municipioId: number;
    municipio: Municipio;
    subastaId: number;
    subasta: Subasta;
    imagen: File;
    video: string;
    lotesDto: Lote[];

    constructor() { }
}
