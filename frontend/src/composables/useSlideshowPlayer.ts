import { computed, ref, type Ref } from 'vue'
import type { MediaDto } from '@/types/api'

/**
 * Gestiona el estado y la reproducción del slideshow.
 */
export function useSlideshowPlayer(
  mediaItems: Readonly<Ref<MediaDto[]>>,
  slideshowDurationSeconds: Readonly<Ref<number>>,
) {
  const activeIndex = ref<number | null>(null)
  const isPlaying = ref(false)
  let slideshowTimer: number | undefined

  const activeItem = computed(() =>
    activeIndex.value === null ? null : (mediaItems.value[activeIndex.value] ?? null),
  )

  /**
   * Detiene el timer activo del slideshow.
   */
  function clearSlideshowTimer() {
    if (slideshowTimer) {
      window.clearInterval(slideshowTimer)
      slideshowTimer = undefined
    }
  }

  /**
   * Abre el item seleccionado si el índice es válido.
   */
  function openItem(index: number) {
    if (index < 0 || index >= mediaItems.value.length) return
    activeIndex.value = index
  }

  /**
   * Abre el primer item de la lista.
   */
  function openFirst() {
    if (mediaItems.value.length === 0) return
    openItem(0)
  }

  /**
   * Cierra el visor y detiene la reproducción.
   */
  function closeViewer() {
    isPlaying.value = false
    clearSlideshowTimer()
    activeIndex.value = null
  }

  /**
   * Avanza al siguiente item.
   */
  function nextItem() {
    if (mediaItems.value.length === 0 || activeIndex.value === null) return
    activeIndex.value = (activeIndex.value + 1) % mediaItems.value.length
  }

  /**
   * Retrocede al item anterior.
   */
  function prevItem() {
    if (mediaItems.value.length === 0 || activeIndex.value === null) return
    activeIndex.value = (activeIndex.value - 1 + mediaItems.value.length) % mediaItems.value.length
  }

  /**
   * Alterna la reproducción automática.
   */
  function togglePlay() {
    if (activeIndex.value === null || mediaItems.value.length === 0) return

    isPlaying.value = !isPlaying.value
    clearSlideshowTimer()

    if (isPlaying.value) {
      slideshowTimer = window.setInterval(() => {
        nextItem()
      }, slideshowDurationSeconds.value * 1000)
    }
  }

  /**
   * Ajusta el índice si los medios cambiaron.
   */
  function syncWithMediaItems() {
    if (activeIndex.value !== null && activeIndex.value >= mediaItems.value.length) {
      closeViewer()
    }
  }

  /**
   * Limpia recursos cuando se desmonta el componente.
   */
  function dispose() {
    clearSlideshowTimer()
  }

  return {
    activeItem,
    isPlaying,
    openItem,
    openFirst,
    closeViewer,
    nextItem,
    prevItem,
    togglePlay,
    syncWithMediaItems,
    dispose,
  }
}
