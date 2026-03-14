# React App

A modern React application built with Vite, TypeScript, and Vitest.

## Getting Started

### Prerequisites
- Node.js (v18+)
- npm or yarn

### Installation

Install dependencies:
```bash
npm install
```

### Available Scripts

**Development**
```bash
npm run dev
```
Runs the app in development mode with hot module replacement.

**Build**
```bash
npm run build
```
Builds the app for production to the `dist` folder.

**Preview**
```bash
npm run preview
```
Serves the production build locally for preview.

**Testing**
```bash
npm run test
```
Runs the test suite in watch mode.

**Test UI**
```bash
npm run test:ui
```
Runs tests with Vitest UI for visual test tracking.

**Linting**
```bash
npm run lint
```
Checks code for linting errors using ESLint.

## Project Structure

```
src/
├── App.tsx              # Main App component
├── App.test.tsx         # App tests
├── App.module.css       # App styles (CSS Modules)
├── main.tsx             # React entry point
├── index.css            # Global styles
├── vite-env.d.ts        # Vite type definitions
└── test/
    └── setup.ts         # Test setup file
```

## Features

- **Vite**: Lightning-fast build tool
- **React 18**: Latest React features with hooks
- **TypeScript**: Static type checking
- **Vitest**: Fast unit testing framework
- **React Testing Library**: Testing utilities
- **ESLint**: Code quality and style enforcement
- **CSS Modules**: Scoped styling

## Configuration Files

- `vite.config.ts` - Vite build configuration
- `vitest.config.ts` - Vitest test runner configuration
- `tsconfig.json` - TypeScript configuration
- `.eslintrc.cjs` - ESLint configuration
