using GameWorkstore.Patterns;
using System.Linq;

namespace GameWorkstore.ProtocolAudio
{
    public class BGMRuntimeState
    {
        public int Hash;
        public bool Active;
        public int Layer;
    }

    public class BGMService : IService
    {
        public const int NotSharedLayer = -1;

        private readonly HighSpeedArray<BGMRuntimeState> _states = new HighSpeedArray<BGMRuntimeState>(32);
        private int _hash;
        private int _layer;

        public override void Preprocess()
        {
        }

        public override void Postprocess()
        {
        }

        public void Play(AudioName audioName)
        {
            _hash = audioName.Hash;
            var bgmRuntimeState = _states.FirstOrDefault(IsEqual);
            if (bgmRuntimeState == null)
            {
                bgmRuntimeState = new BGMRuntimeState() { Hash = _hash, Active = true, Layer = NotSharedLayer };
                _states.Add(bgmRuntimeState);
                return;
            }
            _layer = bgmRuntimeState.Layer;
            _states.ForEach(DisableIfNecessary);
            bgmRuntimeState.Active = true;
        }

        public void Stop(AudioName audioName)
        {
            _hash = audioName.Hash;
            var bgmRuntimeState = _states.FirstOrDefault(IsEqual);
            if (bgmRuntimeState == null) return;
            bgmRuntimeState.Active = false;
        }

        private bool IsEqual(BGMRuntimeState state)
        {
            return state.Hash == _hash;
        }

        private void DisableIfNecessary(BGMRuntimeState state)
        {
            state.Active &= state.Layer != _layer || state.Layer == NotSharedLayer;
        }

        public bool IsPlaying(AudioName name)
        {
            return IsPlaying(name.Hash);
        }

        public bool IsPlaying(int nameHash)
        {
            _hash = nameHash;
            return _states.Any(IsActive);
        }

        private bool IsActive(BGMRuntimeState state)
        {
            if (state.Hash == _hash) return state.Active;
            return false;
        }

        internal void RegisterBGM(AudioName name, int layer)
        {
            _hash = name.Hash;
            var bgmRuntimeState = _states.FirstOrDefault(IsEqual);
            if (bgmRuntimeState == null)
            {
                _states.Add(new BGMRuntimeState() { Hash = _hash, Layer = layer, Active = false });
            }
            else if (layer >= 0)
            {
                bgmRuntimeState.Layer = layer;
            }
        }
    }
}
