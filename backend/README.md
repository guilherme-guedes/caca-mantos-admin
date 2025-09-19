# TODO

## Negócio
- Retorno de consulta de time com homonimos preenchidos e pai <<
- Endpoint para listar times elegíveis para serem homônimos (não principais e não homônimos de outro);

## Geral
- Implementar demais endpoints << TESTAR
- Migrar banco mongo dev para cloud (mesmo server prod ? ver outro usuario) <<

- middleware para log, cache e validação
- testes integração
- Configurar / atualizar start e deploy (projeto infra) considerando postgres e mongo
- Fila para recibmento de alterações (novo formato - deadleter e retry)
- Outbox para resolver depois persistência nos bancos (redis, mongo e postgres)
- CI (Github actions)
- Testes e2e
- Analise de codigo (cobertura, badsmells, qtd linhas classe, metodo, etc)
- Logs (estruturados)
- Metricas e observabilidade