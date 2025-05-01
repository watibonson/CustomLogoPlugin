const fs = require('fs-extra');
const path = require('path');
const archiver = require('archiver');

// Configuration
const pluginName = 'CustomLogoPlugin';
const pluginVersion = '1.0.0';

console.log('Preparing to package the plugin...');

// Ensure the dist directory exists
const distDir = path.join(__dirname, 'dist');
const binDir = path.join(distDir, 'bin');
const packageDir = path.join(distDir, 'package');

if (!fs.existsSync(binDir)) {
  console.error('Error: Build directory not found!');
  console.error('Please run "npm run build" first to build the plugin.');
  process.exit(1);
}

// Create package directory
fs.ensureDirSync(packageDir);

// Read manifest to get metadata
let manifest;
try {
  manifest = fs.readJsonSync(path.join(binDir, 'manifest.json'));
} catch (err) {
  console.error('Error reading manifest.json:', err.message);
  process.exit(1);
}

// Create zip file
const outputFile = path.join(packageDir, `${pluginName}_${pluginVersion}.zip`);
const output = fs.createWriteStream(outputFile);
const archive = archiver('zip', {
  zlib: { level: 9 } // Maximum compression
});

output.on('close', () => {
  console.log(`Package created successfully: ${outputFile}`);
  console.log(`Total size: ${(archive.pointer() / 1024 / 1024).toFixed(2)} MB`);
  
  // Generate repository JSON
  const repoEntry = {
    guid: manifest.guid,
    name: manifest.name,
    description: manifest.description,
    overview: manifest.overview,
    owner: "YourName",
    category: "General",
    versions: [
      {
        version: pluginVersion,
        changelog: "Initial release",
        targetAbi: manifest.targetAbi || "10.8.0.0",
        sourceUrl: `https://example.com/downloads/${pluginName}_${pluginVersion}.zip`,
        checksum: "SHA512:" + require('crypto').createHash('sha512').update(fs.readFileSync(outputFile)).digest('hex'),
        timestamp: new Date().toISOString()
      }
    ]
  };
  
  fs.writeJsonSync(path.join(packageDir, 'repository.json'), repoEntry, { spaces: 2 });
  console.log('Repository JSON file created. Use this to distribute your plugin.');
});

archive.on('error', (err) => {
  throw err;
});

archive.pipe(output);

// Add the bin directory contents to the zip
archive.directory(binDir, false);

// Finalize
archive.finalize();