module.exports = {
  content: ["./Views/**/*.{cshtml,razor,js}"],
  safelist: [
    'input-validation-error'
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require("@tailwindcss/forms")({
      strategy: "class",
    }),
  ],
}
