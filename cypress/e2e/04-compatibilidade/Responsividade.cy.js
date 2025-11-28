describe('Teste Rápido de Compatibilidade', () => {
  const devices = [
    { name: 'Smartphone', width: 412, height: 915 },
    { name: 'Tablet', width: 768, height: 1024 },
    { name: 'Notebook', width: 1440, height: 900 }
  ];

  const pages = [
    'http://localhost:5269/Principal/Index',
    'http://localhost:5269/Principal/IndexADM',
    'http://localhost:5269/Principal/Criar',
    'http://localhost:5269/Computador/CriarPC',
    'http://localhost:5269/Principal/ListarADM',
    'http://localhost:5269/Principal/Dashboard',
    'http://localhost:5269/Principal/Editar'
  ];

  devices.forEach(device => {
    it(`testa todas as páginas em ${device.name}`, () => {
      cy.viewport(device.width, device.height);

      pages.forEach(page => {
        cy.visit(page);
        cy.get('body').should('be.visible');
        
        // Verifica layout básico
        cy.get('header').should('exist');
        cy.get('main, .container, section').should('exist');
        
        // Screenshot para documentação
        const pageName = page.split('/').pop();
        cy.screenshot(`${device.name}-${pageName}`);
        
        cy.wait(300);
      });
    });
  });
});