module.exports = {
  content: [
     './Pages/**/*.cshtml',
     './Views/**/*.cshtml'
],
  theme: {
      extend: {},
  },
  plugins: [require('@tailwindcss/forms')],
}

