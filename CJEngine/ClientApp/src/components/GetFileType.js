﻿import React, { Component } from 'react';

export function GetFileType(filename) {
    if (filename != undefined) {
        var x = filename.includes("pdf");
    }
    else {
        x = false;
    }
    return x;
}