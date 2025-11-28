describe('Exclusão de Computador pelo Administrador', () => {
  beforeEach(() => {
    cy.visit('http://localhost:5269/Principal/IndexADM')
    cy.contains('Lista de Notificações').should('be.visible')
  })

  it('deve excluir o computador com nome C01-LAB1-5605', () => {
    // 1. Acessa "Lista de Notificações"
    cy.contains('Lista de Notificações').click()
    cy.url().should('include', '/Principal/ListarADM')

    // 2. Clica em "Editar"
    cy.get('#editar').contains('Editar').click()
    cy.url().should('include', '/Principal/Editar')

    // 3. Encontra e clica no botão de deletar do computador com nome C01-LAB1-5605
    cy.contains('li', 'Nome: C01-LAB1-5605')
      .find('.btn-delete')
      .click()

    // 4. Verifica página de confirmação
    cy.url().should('include', '/Principal/Deletar')
    cy.contains('Você tem certeza que deseja DELETAR o computador').should('be.visible')

    // 5. Confirma a exclusão
    cy.contains('button', 'Sim').click()

    // 6. Verifica redirecionamento
    cy.url().should('not.include', '/Deletar')
  })
})