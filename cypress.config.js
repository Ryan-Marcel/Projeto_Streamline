const { defineConfig } = require("cypress");

module.exports = defineConfig({
  e2e: {
    setupNodeEvents(on, config) {
      this.baseUrl = "http://localhost:3000";
      // implement node event listeners here
    },
  },
});
