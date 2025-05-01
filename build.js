const { execSync } = require('child_process');
const fs = require('fs-extra');
const path = require('path');

// Configuration
const pluginName = 'CustomLogoPlugin';
const pluginVersion = '1.0.0';
const targetFramework = 'net6.0'; // Jellyfin 10.8.0+ uses .NET 6.0

// Ensure output directories exist
const outputDir = path.join(__dirname, 'dist');
const buildDir = path.join(outputDir, 'build');

console.log('Cleaning previous build files...');
fs.removeSync(outputDir);
fs.ensureDirSync(buildDir);

// Copy source files to build directory
console.log('Copying source files...');
fs.copySync('./CustomLogoPlugin-1.0.0', buildDir, {
  filter: (src) => {
    // Exclude any temp or system files
    return !src.includes('obj') && !src.includes('bin');
  }
});

// Detect if dotnet is available
try {
  console.log('Checking .NET SDK...');
  execSync('dotnet --version', { stdio: 'pipe' });
  
  // If dotnet is available, build the plugin
  console.log('Building plugin...');
  execSync(`dotnet publish "${path.join(buildDir, 'CustomLogoPlugin.csproj')}" -c Release -f ${targetFramework} --output "${path.join(outputDir, 'bin')}"`, { 
    stdio: 'inherit'
  });
  
  console.log('Build completed successfully!');
} catch (error) {
  console.error('Error: .NET SDK not found or build failed.');
  console.error('To build this plugin, you need to install the .NET SDK version 6.0 or higher.');
  console.error('Download from: https://dotnet.microsoft.com/download');
  console.error('');
  console.error('Detailed error:', error.message);
  process.exit(1);
}

// Copy additional files needed for Jellyfin
console.log('Copying metadata files...');
fs.copySync(
  path.join(buildDir, 'manifest.json'), 
  path.join(outputDir, 'bin', 'manifest.json')
);

fs.copySync(
  path.join(buildDir, 'static'), 
  path.join(outputDir, 'bin', 'static')
);

console.log(`Plugin built successfully: ${outputDir}/bin`);