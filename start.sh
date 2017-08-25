#!/bin/bash

clear

echo ">>> building client-side web app & Widget"
npm run build


echo ">>> building Go app"
rm parle-dev
go build -o parle-dev

echo ">>> app started"
./parle-dev

