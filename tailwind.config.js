module.exports = {
  content: [
     './Pages/**/*.cshtml',
     './Views/**/*.cshtml'
],
  theme: {
      extend: {colors: {
        primary: {
          light: '#C3FCF2', // light primary color
          DEFAULT: '#02B6A4', // primary color
          dark: '#008070', // dark primary color
        },
        secondary: {
          light: '#A78BFA', // light secondary color
          DEFAULT: '#7C3AED', // secondary color
          dark: '#4A1E96', // dark secondary color
        },
        accent: {
          light: '#FFE6FF', // light accent color
          DEFAULT: '#820081', // accent color
          dark: '#B45309', // dark accent color
        }
      }
    },
  },
  plugins: [require('@tailwindcss/forms')],
}

