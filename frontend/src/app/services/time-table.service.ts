import { Injectable } from "@angular/core";
import { IColunaTabela } from "../shared/components/tabela-dinamica/tabela-dinamica.component";

@Injectable({
    providedIn: 'root'
})
export class TimeTableService {
    criarColunasTabelaResumida(): IColunaTabela[] {
        return  [
            { key: 'identificador', label: 'Identificador', bool: false },
            { key: 'nome', label: 'Nome', bool: false }
        ]
    }
    
    criarColunasTabelaCompleta(): IColunaTabela[] {
        return  [
            { key: 'identificador', label: 'Identificador', bool: false },
            { key: 'nome', label: 'Nome', bool: false }, 
            { key: 'ativo', label: 'Ativo', bool: true },           
            { key: 'principal', label: 'Principal', bool: true },
            { key: 'destaque', label: 'Destaque', bool: true }
        ]
    }
}