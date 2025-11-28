describe('Navegação do Aluno', () => {
  beforeEach(() => {
    // Faz login e vai para a página principal
    cy.visit('http://localhost:5269')
    cy.get('#username').type('aluno123')
    cy.get('#password').type('senha123')
    cy.contains('button', 'Entrar').click()
    
    // Espera carregar a página principal
    cy.contains('Criar Notificação').should('be.visible')
  })

  it('deve acessar todos os links e voltar para o index', () => {
    // 1. Acessa "Criar Notificação"
    cy.contains('Criar Notificação').click()
    cy.url().should('include', '/Principal/Criar')
    cy.go('back') // Volta para o index

    // 2. Acessa "Editar Notificações" 
    cy.contains('Editar Notificações').click()
    cy.url().should('include', '/Computador/CriarPC')
    cy.go('back') // Volta para o index

    // 3. Acessa "Histórico de Notificações"
    cy.contains('Histórico de Notificações').click()
    cy.url().should('include', '/Principal/ListarADM')
    cy.go('back') // Volta para o index

    // Verifica que voltou para a página principal
    cy.contains('Criar Notificação').should('be.visible')
    cy.contains('Editar Notificações').should('be.visible')
    cy.contains('Histórico de Notificações').should('be.visible')
  })
})