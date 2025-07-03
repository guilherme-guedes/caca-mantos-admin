import { Injectable } from "@angular/core";
import { IColunaTabela } from "../shared/components/tabela-dinamica/tabela-dinamica.component";

@Injectable({
    providedIn: 'root'
})
export class LojaTableService {
    criarColunasTabelaResumida(): IColunaTabela[] {
        return  [
            { key: 'nome', label: 'Nome', bool: false },
            { key: 'site', label: 'Site', bool: false }
        ]
    }
    
    criarColunasTabelaCompleta(): IColunaTabela[] {
        return  [
            { key: 'nome', label: 'Nome', bool: false },
            { key: 'site', label: 'Site', bool: false },
            { key: 'ativa', label: 'Ativa', bool: true },
            { key: 'parceira', label: 'Parceira', bool: true }
        ]
    }
}