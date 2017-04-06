#!/bin/bash

clear

echo ">>> building Go app"
rm parle-dev
go build -o parle-dev

echo ">>> app started"
./parle-dev

