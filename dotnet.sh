#!/bin/bash

cd /home/fredericorook-net/build/

QUANT_RUNTIME=$(ls -l *runtimeconfig* | wc -l)

if [[ "$QUANT_RUNTIME" > 1 ]]; then
    DOTNET=$(ls *runtimeconfig* 2>/dev/null | grep -v Alfasoft | sed 's/runtimeconfig.json/dll/g')
else
    DOTNET=$(ls *runtimeconfig* 2>/dev/null | sed 's/runtimeconfig.json/dll/g')
fi

if [ "$DOTNET" != ".dll" ]; then
    echo "$(date) ######################################################" >> /home/fredericorook-net/logs/dotnet.log
    dotnet $DOTNET --urls="http://0.0.0.0:$DOTNET_PORT" >> /home/fredericorook-net/logs/dotnet.log
else
    echo "Projeto não localizado em '/home/fredericorook-net/build'" >> /home/fredericorook-net/logs/dotnet.log
fi
