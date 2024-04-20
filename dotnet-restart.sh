#!/bin/bash

while true; do
    if [ -f "/home/fredericorook-net/build/restart" ]; then
        supervisorctl stop fredericorook-net-dotnet
        supervisorctl start fredericorook-net-dotnet
        rm -f "/home/fredericorook-net/build/restart"
    fi

    sleep 1s
done