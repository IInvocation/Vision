1. Copy this script somewhere out of the repository
2. Change the script using:
- a useful CA-Subject
- a secure Password
3. Run a VisualStudio command prompt
4. Import the generated *.pfx-file into Trusted-CA's for the local MACHINE
5. Set IDENTITY_SigningCn (either by environment variable or json-config to the configured CA-Subject
6. Backup all generated files and try to remember the password