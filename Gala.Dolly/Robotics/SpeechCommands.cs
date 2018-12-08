namespace Gala.Dolly.Robotics.BS2Commands
{
    internal enum SpeechCommands : byte
    {
        None = 0,
        Pin1On = 11,
        Pin1Off = 10,
        Pin2On = 21,
        Pin2Off = 20,
        Pin4On = 41,
        Pin4Off= 40,
        Pin5On = 51,
        Pin5Off= 50,
        MouthPositionClosed = 90,
        MouthPositionOpen1 = 91,
        MouthPositionOpen2 = 92,
        MouthPositionOpen3 = 93,
        MouthPositionOpen4 = 94,
        MouthPositionLittleSmile = 95,
        MouthPositionBigSmile= 96,
        MouthPositionOoh = 97,
        MouthPositionLittleFrown = 98,
        MouthPositionBigFrown = 99
    }
}
