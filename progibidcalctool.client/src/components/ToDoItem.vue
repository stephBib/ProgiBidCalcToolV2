<template>
  <div class="form-check">
    <input class="form-check-input" id="todo-item" type="checkbox" value="">
    <label class="form-check-label" for="todo-item">
      Default checkbox
    </label>
  </div>
</template>

<script>
  export default {
    props: {
      label: { required: true, type: String },
      done: { default: false, type: Boolean },
    },
  };
</script>
