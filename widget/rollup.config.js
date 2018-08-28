import nodeResolve from 'rollup-plugin-node-resolve';
import typescript from 'rollup-plugin-typescript';
import {uglify} from 'rollup-plugin-uglify';

export default {
  input: './init.tsx',
  plugins: [
    typescript({
      // So we use the local version of typescript
      typescript: require('typescript')
    }),
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
