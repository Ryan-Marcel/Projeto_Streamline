it('Navega por todos os links do admin', () => {
  // Acessa diretamente a página do admin
  cy.visit('http://localhost:5269/Principal/IndexADM')

  // Navegação rápida por todos os links
  cy.contains('Criar Sala e Computador').click()
  cy.go('back')
  
  cy.contains('Salas e Computadores').click()
  cy.go('back')
  
  cy.contains('Lista de Notificações').click()
  cy.go('back')
  
  cy.contains('Dashboard').click()
  cy.go('back')
})