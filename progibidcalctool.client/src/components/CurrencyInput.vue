<template>
  <input ref="inputRef" :value="formattedValue">
</template>

<script>
  import { useCurrencyInput } from 'https://cdn.jsdelivr.net/npm/vue-currency-input@3.1.0/+esm'
  export default {
    name: "CurrencyInput",
    props: {
      modelValue: Number,
      options: Object,
    },
    setup(props) {
      const {
        inputRef,
        formattedValue,
        setOptions,
        setValue
      } = useCurrencyInput(props.options)

      return { inputRef, formattedValue }
    }
  };
</script>
