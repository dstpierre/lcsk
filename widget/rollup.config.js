import nodeResolve from 'rollup-plugin-node-resolve';
import { uglify } from 'rollup-plugin-uglify';

export default {
  input: './init.js',
  plugins: [
    nodeResolve({
      jsnext: true
    }),
    uglify()
  ],
  output: {
    file: './dist/widget.js',
    sourcemap: true,
    format: 'iife'
  }
};
