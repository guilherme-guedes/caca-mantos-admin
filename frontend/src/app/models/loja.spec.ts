import { Loja } from './loja';

describe('Loja', () => {
  it('should create an instance', () => {
      const loja: Loja ={
        id: '1',
        nome: 'Memórias do Esporte',
        site: 'memoriasdoesporte.com.br',
        times: ['fluminense-rj', 'botafogo-rj', 'vasco-rj', 'flamengo-rj'],
        urlBusca: 'https://memoriasdoesporte.com.br/busca?time=',
        parceira: true,
        ativa: true        
      };
  
      expect(loja.id).toBe('1');
      expect(loja.nome).toBe('Memórias do Esporte');
      expect(loja.site).toBe('memoriasdoesporte.com.br');
      expect(loja.times).toContain('fluminense-rj');
      expect(loja.times).toContain('botafogo-rj');
      expect(loja.times).toContain('vasco-rj');
      expect(loja.times).toContain('flamengo-rj');
      expect(loja.urlBusca).toBe('https://memoriasdoesporte.com.br/busca?time=');
      expect(loja.parceira).toBeTrue();
      expect(loja.ativa).toBeTrue();
  });
});
