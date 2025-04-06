# How to build and use docker image

## Build image

The image is based on the `python:3.11-slim` image and we install the required dependencies for bluetooth as well as the `poetry` installer.

To build the image with the tag `niimprint`:

```sh
docker build -t niimprint .
```
  
## Run the container

To run the container and go in the internal shell, run the command:

```
docker run -it --rm -v ${PWD}/images:/app/images --privileged --device=dev/ttyACM1:/dev/ttyACM1 --entrypoint=/bin/bash niimprint
```

This command:

- maps a volume from th folder `./images/` to the folder `/app/images/` in the container
- forwards the USB device `dev/ttyACM1` to the same path into the container

## Execute print command

Two options to print a label:

### Print from inside the container

Execute the previous command to run the container shell and then print with a command similar to:

```sh
python niimprint -m b1 -i images/test_mire_50x30.png -c usb -a /dev/ttyACM1
```

### Print from outside the container

**TODO**

## Use bluetooth

**TODO**

