import { DecimalPipe } from "@angular/common";
import { Categoria } from "./categoria";
import { Raza } from "./raza";
import { Sexo } from "./sexo";
import { Municipio } from "./municipio";
import { Lote } from "./lote";

export class Animal {

    animalId: string;
    descripcion: string;
    foto: string;
    peso: number;
    categoriaId: number;
    categoria: Categoria;
    razaId: number;
    raza: Raza;
    sexoId: number;
    sexo: Sexo;
    municipioId: number;
    municipio: Municipio;
    loteId: number;
    lote: Lote;
    imagen: File;
    video: string;
    constructor(){}
}