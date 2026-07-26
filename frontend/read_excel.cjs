const { execSync } = require('child_process');

const filePath = '/Users/shashank/cm-fellow-scheme/CM Fellow Module Pages Filled Validation.xlsx';

const workbook = execSync(`unzip -p "${filePath}" xl/workbook.xml`).toString();
const sheetMatches = workbook.match(/<sheet [^>]*name="([^"]+)"/g) || [];

console.log('--- SHEET NAMES ---');
sheetMatches.forEach((s) => {
  const name = s.match(/name="([^"]+)"/)[1];
  console.log('Sheet:', name);
});
