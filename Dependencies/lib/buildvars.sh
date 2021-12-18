#!/bin/bash

THREADCOUNT=8
PLATFORM=$($(command -v uname) | tr '[:upper:]' '[:lower:]')

CMAKE=cmake
CMAKE_GENERATOR="Unix Makefiles"
CMAKE_FLAGS=-Wno-dev

MAKE=make

FT_TAG=VER-2-11-0
HB_TAG=2.9.0
SDL_VER=0.18
SDL_TAG=release-2.$SDL_VER

NATIVES_DIR=$SCRIPT_DIR/build_env
PATCH_DIR=$SCRIPT_DIR/patches
CMAKE_SCRIPT_DIR=$SCRIPT_DIR/scripts

SOURCES_DIR=$NATIVES_DIR/sources
BUILDROOT_DIR=$NATIVES_DIR/buildroot
ARTIFACT_DIR=$NATIVES_DIR/artifacts

HB=harfbuzz
HB_PATH=$SOURCES_DIR/$HB
HB_BUILDROOT=$BUILDROOT_DIR/$HB
HB_ARPATH=$HB_BUILDROOT/libharfbuzz.a
HB_BUILD_TYPE=Release
HB_ARTIFACT=libharfbuzz.a

FT=freetype
FT_PATH=$SOURCES_DIR/$FT
FT_BUILDROOT=$BUILDROOT_DIR/$FT
FT_BUILD_TYPE=Release

SDL=SDL
SDL_PATH=$SOURCES_DIR/$SDL
SDL_BUILDROOT=$BUILDROOT_DIR/$SDL
SDL_BUILD_TYPE=Release

SDL_gpu=SDL_gpu
SDL_GPU_PATH=$SOURCES_DIR/$SDL_gpu
SDL_GPU_BUILDROOT=$BUILDROOT_DIR/$SDL_gpu
SDL_GPU_BUILD_TYPE=Release

SDL_sound=SDL_sound
SDL_SOUND_PATH=$SOURCES_DIR/$SDL_sound
SDL_SOUND_BUILDROOT=$BUILDROOT_DIR/$SDL_sound
SDL_SOUND_BUILD_TYPE=Release

SDL_nmix=SDL_nmix
SDL_NMIX_PATH=$SOURCES_DIR/$SDL_nmix
SDL_NMIX_BUILDROOT=$BUILDROOT_DIR/$SDL_nmix
SDL_NMIX_BUILD_TYPE=Release
