# MushROMs (2012)
An archive of MushROMs' design strategy from 2012

**This program currently doesn't build completely and is for archive purposes only. If you want to work on this, then that's alright too.**

## About
This project went underway when I leraned that program code and GUI code shouldn't be mixed. I tried to make the program as agnostic as possible so it could be ported if possible. However, I still was trying to mix it with unmanaged code. I wasn't trusting the power of managed arrays and was convinced unmanaged pointers were the only way to ensure the fastest possible code (even if fast wasn't absolutely necessary).

The project was abandoned when it became very obvious that unsafe pointers were a poor design choice, yet they were too integrated into the project to simply modify.
